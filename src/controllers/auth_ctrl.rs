use actix_web::{
    post,
    web::{self, Json},
    HttpResponse,
};
use mongodb::bson::doc;
use mongodb::options::FindOneOptions;
use crate::{repository::mongodb_repo::MongoDBRepo};
use crate::models::data::user_model::User;
use crate::models::request::login_req_model::LoginReq;
use crate::models::request::register_req_model::RegisterReq;
use crate::models::response::auth_res_model::AuthRes;
use crate::models::response::user_res_model::UserRes;
use crate::utilities::auth_utils::generate_auth_token;

#[post("/auth/login")]
pub async fn login(db_client: web::Data<MongoDBRepo>, payload: Json<LoginReq>) -> HttpResponse {
    let query_opts = FindOneOptions::builder().build();
    let query = doc! {
        "email_address": doc! { "$regex": payload.email_address.to_owned(), "$options": "i" }
    };
    let result = db_client.users.find_one(query, query_opts).await;

    return match result {
        Ok(user_response) => {
            return match user_response {
                Some(user) => {
                    if user.validate_password(payload.password.to_owned()) {
                        return HttpResponse::Ok().json(AuthRes {
                            token: generate_auth_token(user.id.unwrap()),
                            user: UserRes::map_from_data_model(user)
                        });
                    }

                    return HttpResponse::BadRequest().body("Invalid password");
                }
                None => HttpResponse::NotFound().body("No user account found")
            };
        }
        Err(err) => HttpResponse::InternalServerError().body(err.to_string()),
    };
}

#[post("/auth/register")]
pub async fn register(db_client: web::Data<MongoDBRepo>, payload: Json<RegisterReq>) -> HttpResponse {
    // let user_exists = db_client.users.find_one()

    let data = User::new(
        payload.first_name.to_owned(),
        payload.last_name.to_owned(),
        payload.email_address.to_owned(),
        payload.password.to_owned(),
    );
    let result = db_client.users.insert_one(data, None).await;

    return match result {
        Ok(user) => HttpResponse::Ok().json(user),
        Err(err) => HttpResponse::InternalServerError().body(err.to_string()),
    };
}