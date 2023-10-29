extern crate dotenv;

use mongodb::{Client, Collection};
use crate::models::data::user_model::User;

pub struct MongoDBRepo {
    pub users: Collection<User>,
}

impl MongoDBRepo {
    pub async fn init() -> Self {
        let db_url = match dotenv::var("DATABASE_URI") {
            Ok(x) => x.to_string(),
            Err(_) => format!("Error reading the database url")
        };
        let client = Client::with_uri_str(db_url).await
            .expect("Error connection to the database");
        let db = client.database("facility-hub");

        return MongoDBRepo {
            users: db.collection("users")
        };
    }
}