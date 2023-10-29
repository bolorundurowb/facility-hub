use mongodb::bson::{self, oid::ObjectId};
use serde::{Deserialize, Serialize};
use bcrypt::{DEFAULT_COST, hash, verify};

#[derive(Deserialize, Serialize)]
pub struct User {
    #[serde(rename = "_id", skip_serializing_if = "Option::is_none")]
    pub id: Option<ObjectId>,
    pub first_name: Option<String>,
    pub last_name: Option<String>,
    pub email_address: String,
    pub password_hash: String,
    pub joined_at: bson::DateTime,
}

impl User {
    pub fn new(first_name: Option<String>, last_name: Option<String>, email_address: String, password: String) -> Self {
        let normalized_email = email_address.trim().to_lowercase();
        let password_hash = hash(password, DEFAULT_COST).ok().expect("Error when hashing password");

        return User {
            id: None,
            first_name,
            last_name,
            email_address: normalized_email,
            password_hash,
            joined_at: bson::DateTime::now(),
        };
    }

    pub fn validate_password(&self, password: String) -> bool {
        return verify(password, &self.password_hash.to_string())
            .is_ok();
    }
}
