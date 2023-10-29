use actix_web::{
    post,
    web::{self, Json},
    HttpResponse,
};
use crate::{repository::mongodb_repo::MongoDBRepo};
use crate::models::data::user_model::User;
use crate::models::request::register_req_model::RegisterReq;

#[post("/auth/register")]
pub async fn register(db_client: web::Data<MongoDBRepo>, payload: Json<RegisterReq>) -> HttpResponse {
    // let user_exists = dbClient.users.exists();
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