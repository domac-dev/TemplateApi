namespace Domain
{
    public static class ModelConstants
    {
        public static class UserModel
        {
            public const int USERNAME = 25;
            public const int PASSWORD = 50;
        }

        public static class RoleModel
        {
            public const int NAME = 15;

        }
        public static class ClaimModel
        {
            public const int NAME = 15;
        }

        public static class TokenModel
        {
            public const int VALUE = 50;
            public const int REVOKE_REASON = 255;
            public const int IP_ADDRESS = 15;
        }

        public static class GeneralModel
        {
            public const int GUID = 36;
            public const int MAX = 255;
        }
    }
}
