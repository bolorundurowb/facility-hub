use serde::{Deserialize, Serialize};

#[derive(Deserialize)]
pub struct RegisterReq {
    pub first_name: Option<String>,
    pub last_name: Option<String>,
    pub email_address: String,
    pub password: String,
}