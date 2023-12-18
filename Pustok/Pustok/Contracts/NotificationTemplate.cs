namespace Pustok.Contracts;

public class NotificationTemplate
{
    public class Order
    {
        public class Created
        {
            public const string TITLE = "New order created";
            public const string CONTENT = $"There is a new order #{NotificationTemplateKeyword.TRACKING_CODE} from {NotificationTemplateKeyword.USER_FULL_NAME}";
        }

        public class Updated
        {
            public const string TITLE = "Your order's status updated";
            public const string CONTENT = $"Your order #{NotificationTemplateKeyword.TRACKING_CODE} status updated to {NotificationTemplateKeyword.ORDER_STATUS_NAME}";
        }
    }

    public class Broadcast
    {
        public const string TITLE = "General notification";
    }
}

public class NotificationTemplateKeyword
{
    public const string TRACKING_CODE = "{order_tracking_code}";
    public const string USER_FULL_NAME = "{user_full_name}";
    public const string ORDER_STATUS_NAME = "{order_status_name}";
}
