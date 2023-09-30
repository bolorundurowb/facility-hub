use serde::{Deserialize, Serialize};
use mongodb::bson::DateTime;
use bcrypt::{DEFAULT_COST, hash, verify};

pub mod models {
    #[derive(Deserialize, Serialize)]
    struct User {
        pub first_name: Option<String>,
        pub last_name: Option<String>,
        pub email_address: String,
        pub password_hash: String,
        created_at: DateTime,
    }

    impl User {
        pub fn new(first_name: Option<String>, last_name: Option<String>, email_address: String, password: String) -> Self {
            let normalized_email = email_address.trim().to_lowercase();
            let password_hash = hash(password, DEFAULT_COST);

            return User {
                first_name,
                last_name,
                email_address: normalized_email,
                password_hash,
                created_at: DateTime(Utc::now())
            };
        }
    }
}

