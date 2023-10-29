use serde::{Deserialize, Serialize};

#[derive(Deserialize, Serialize)]
pub struct LoginReq {
    pub email_address: String,
    pub password: String,
}