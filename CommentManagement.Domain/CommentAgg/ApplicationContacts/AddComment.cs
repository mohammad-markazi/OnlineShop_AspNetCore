namespace CommentManagement.Domain.CommentAgg.ApplicationContacts
{
    public class AddComment
    {
        public string Name { get;  set; }
        public string Email { get;  set; }
        public string Message { get;  set; }
        public long EntityId { get;  set; }
        public EntityType EntityType { get;  set; }
        public long? ParentId { get;  set; }
        public string EntitySlug { get; set; }
    }
}
