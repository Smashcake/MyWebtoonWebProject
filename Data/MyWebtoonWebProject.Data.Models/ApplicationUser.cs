// ReSharper disable VirtualMemberCallInConstructor
namespace MyWebtoonWebProject.Data.Models
{
    using System;
    using System.Collections.Generic;

    using MyWebtoonWebProject.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.SubcribedTo = new HashSet<WebtoonsSubscribers>();
            this.Comments = new HashSet<Comment>();
            this.Reviews = new HashSet<Review>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<WebtoonsSubscribers> SubcribedTo { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
