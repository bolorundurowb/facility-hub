use mongodb::bson::{self, DateTime};
use serde::Serialize;
use crate::models::data::user_model::User;

#[derive( Serialize)]
pub struct UserRes {
    pub id: String,
    pub first_name: Option<String>,
    pub last_name: Option<String>,
    pub email_address: String,
    #[serde(with = "bson::serde_helpers::bson_datetime_as_rfc3339_string")]
    pub joined_at: DateTime,
}

impl UserRes {
    pub fn map_from_data_model(model: User) -> UserRes {
        return UserRes {
            id: model.id.unwrap().to_string(),
            first_name: model.first_name,
            last_name: model.last_name,
            email_address: model.email_address,
            joined_at: model.joined_at
        }
    }
}