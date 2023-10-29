mod controllers;
mod models;
mod repository;
mod utilities;

use actix_web::{App, HttpServer};
use actix_web::web::Data;
use dotenv::dotenv;
use repository::mongodb_repo::MongoDBRepo;
use crate::controllers::auth_ctrl::{login, register};

#[actix_web::main]
async fn main() -> std::io::Result<()> {
    dotenv().ok();

    let port = dotenv::var("PORT")
        .unwrap_or("9876".to_string())
        .parse::<u16>()
        .unwrap();
    let db = MongoDBRepo::init().await;
    let db_data = Data::new(db);

    HttpServer::new(move || {
        App::new()
            .app_data(db_data.clone())
            .service(login)
            .service(register)
    })
        .bind(("127.0.0.1", port))?
        .run()
        .await
}
