﻿namespace FirstWeb.API.Model.DTO
{
    public class Responses
    {
        public record class GeneralResponse(bool flag,string Message);
        public record class LoginResponse(bool flag,string Token,string Message);
    }
}
