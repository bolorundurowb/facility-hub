use actix_web::{
    post,
    web::{self, Json},
    HttpResponse,
};
use crate::{models::user_model::User, repository::mongodb_repo::MongoDBRepo};

#[post("/auth/register")]
pub async fn register(dbClient: web::Data<MongoDBRepo>, payload: Json<RegisterReq>) -> HttpResponse {
    // let user_exists = dbClient.users.exists();
    let data = User::new(
        payload.first_name,
        payload.last_name,
        payload.email_address,
        payload.password,
    );
    let result = dbClient.users.insert_one(data, None).await;

    return match result {
        Ok(user) => HttpResponse::Ok().json(user),
        Err(err) => HttpResponse::InternalServerError().body(err.to_string()),
    };
}