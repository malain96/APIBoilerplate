using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domaine.Base
{
    public interface IBaseEntity<TKey>
    {
        TKey Id { get; set; }
    }

    public interface ISoftDeleteEntity
    {
        bool IsDeleted { get; set; }
        DateTime? DeletedDate { get; set; }
        string DeletedBy { get; set; }
    }

    public interface ISoftDeleteEntity<TKey> : ISoftDeleteEntity, IBaseEntity<TKey>
    {
    }

    public interface IAuditEntity
    {
        DateTime CreatedDate { get; set; }
        string CreatedBy { get; set; }
        DateTime? UpdatedDate { get; set; }
        string UpdatedBy { get; set; }
    }

    public interface IAuditEntity<TKey> : IAuditEntity, IBaseEntity<TKey>
    {
    }

    public interface IAuditDeleteEntity<Tkey> : IAuditEntity, ISoftDeleteEntity
    {
    }

    public abstract class BaseEntity<TKey> : IBaseEntity<TKey>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual TKey Id { get; set; }
    }

    public abstract class SoftDeleteEntity<TKey> : BaseEntity<TKey>, ISoftDeleteEntity<TKey>
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string DeletedBy { get; set; }
    }

    public abstract class AuditEntity<TKey> : BaseEntity<TKey>, IAuditEntity<TKey>
    {
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }

    public abstract class AuditSoftDeleteEntity<TKey> : BaseEntity<TKey>, IAuditEntity<TKey>, ISoftDeleteEntity<TKey>
    {
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string DeletedBy { get; set; }
    }
}