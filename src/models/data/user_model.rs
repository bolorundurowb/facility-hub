use mongodb::bson::{self, oid::ObjectId};
use serde::{Deserialize, Serialize};
use bcrypt::{DEFAULT_COST, hash, verify};

#[derive(Deserialize, Serialize)]
struct User {
    #[serde(rename = "_id", skip_serializing_if = "Option::is_none")]
    pub id: Option<ObjectId>,
    pub first_name: Option<String>,
    pub last_name: Option<String>,
    pub email_address: String,
    pub password_hash: String,
    pub created_at: Option<bson::DateTime>,
    pub updated_at: Option<bson::DateTime>,
}

impl User {
    pub fn new(first_name: Option<String>, last_name: Option<String>, email_address: String, password: String) -> Self {
        let normalized_email = email_address.trim().to_lowercase();
        let password_hash = hash(password, DEFAULT_COST);

        return User {
            id: None,
            first_name,
            last_name,
            email_address: normalized_email,
            password_hash,
            created_at: bson::DateTime::now(),
            updated_at: bson::DateTime::now(),
        };
    }
}
