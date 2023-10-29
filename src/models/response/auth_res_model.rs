use serde::{Deserialize, Serialize};
use crate::models::response::user_res_model::UserRes;

#[derive( Serialize)]
pub struct AuthRes {
    pub token: String,
    pub user: UserRes,
}