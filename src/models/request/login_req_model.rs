use serde::{Deserialize, Serialize};

#[derive(Deserialize)]
pub struct LoginReq {
    pub email_address: String,
    pub password: String,
}