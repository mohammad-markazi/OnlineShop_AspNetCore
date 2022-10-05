using System.Collections.Generic;
using _0_Framework.Domain;

namespace CommentManagement.Domain.CommentAgg
{
    public class Comment:EntityBase
    {
        public string Name { get;private set; }

        public string Email { get; private set; }
        public bool IsConfirmed { get; private set; }
        public bool IsCanceled { get; private set; }
        public string Message { get; private set; }
        public long EntityId { get; private set; }
        public EntityType EntityType { get; private set; }

        public long? ParentId { get; private set; }
        public Comment Parent { get; private set; }

        public List<Comment> Children { get; private set; }
        public Comment(string name, string email, string message, long entityId,EntityType entityType,long? parentId)
        {
            Name = name;
            Email = email;
            Message = message;
            EntityId = entityId;
            EntityType = entityType;
            ParentId = parentId;
        }

        public void Confirm()=>IsConfirmed=true;
        public void Canceled()=>IsCanceled=true;

    }

    public enum EntityType
    {
        Article,
        Product
    }
}
