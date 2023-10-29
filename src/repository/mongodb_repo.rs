use std::env;

extern crate dotenv;

use dotenv::dotenv;

use mongodb::{
    bson::{extjson::de::Error},
    results::{InsertOneResult},
    sync::{Client, Collection},
};
use crate::models::user::User;

pub struct MongoDBRepo {
    users: Collection<User>,
}

impl MongoDBRepo {
    pub fn init() -> Self {
        dotenv().ok();
        let db_url = match dotenv::var("DATABASE_URI") {
            Ok(x) => x.to_string(),
            Err(_) => format!("Error reading the database url")
        };
        let client = Client::with_uri_str(db_url).unwrap();
        let db = client.database("facility-hub");

        return MongoDBRepo {
            users: db.collection("users")
        }
    }
}

pub async fn get_db_client() -> Result<Client, Error> {
    dotenv::ok();
    let db_url = dotenv::var("DATABASE_URI").unwrap();
}