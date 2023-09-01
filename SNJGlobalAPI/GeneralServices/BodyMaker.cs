namespace SNJGlobalAPI.GeneralServices
{
    public static class BodyMaker
    {
        public static EmailOptions Make(string email, string subject, string configBody, params object[] p)
        {
            return new()
            {
                recipients = new() { email },
                subject = subject,
                body = String.Format(configBody, p)
            };
        }


    }
}
