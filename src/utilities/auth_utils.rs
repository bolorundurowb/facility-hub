use chrono::Utc;
use jsonwebtoken::{encode, EncodingKey, Header};
use mongodb::bson::oid::ObjectId;
use serde::{Deserialize, Serialize};

#[derive(Debug, Serialize, Deserialize)]
struct Claims {
    sub: String,
    exp: usize,
}

pub fn generate_auth_token(user_id: ObjectId) -> String {
    let secret = match dotenv::var("SECRET") {
        Ok(x) => x.to_string(),
        Err(_) => format!("Error reading the auth secret")
    };
    let expiration = Utc::now()
        .checked_add_signed(chrono::Duration::hours(24))
        .expect("Valid timestamp")
        .timestamp();

    let claims = Claims {
        sub: user_id.to_string(),
        exp: expiration as usize,
    };

    return encode(&Header::default(), &claims, &EncodingKey::from_secret(secret.as_bytes())).unwrap();
}