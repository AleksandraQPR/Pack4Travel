namespace Pack4Travel.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class pack4travel : DbContext
    {
        public pack4travel()
            : base("name=pack4travel")
        {
        }

        public virtual DbSet<equipements> equipements { get; set; }
        public virtual DbSet<itemGroup> itemGroup { get; set; }
        public virtual DbSet<items> items { get; set; }
        public virtual DbSet<userInfo> userInfo { get; set; }
        public virtual DbSet<equipementTags> equipementTags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<equipements>()
                .Property(e => e.equipementName)
                .IsUnicode(false);

            modelBuilder.Entity<equipements>()
                .Property(e => e.privateStatus)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<equipements>()
                .HasMany(e => e.equipementTags)
                .WithRequired(e => e.equipements)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<equipements>()
                .HasMany(e => e.items)
                .WithMany(e => e.equipements)
                .Map(m => m.ToTable("equipementIems").MapLeftKey("idEquipement").MapRightKey("idItem"));

            modelBuilder.Entity<itemGroup>()
                .Property(e => e.groupName)
                .IsUnicode(false);

            modelBuilder.Entity<itemGroup>()
                .HasMany(e => e.items)
                .WithRequired(e => e.itemGroup)
                .HasForeignKey(e => e.idGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<items>()
                .Property(e => e.itemName)
                .IsUnicode(false);

            modelBuilder.Entity<items>()
                .Property(e => e.privateStatus)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<userInfo>()
                .Property(e => e.userName)
                .IsUnicode(false);

            modelBuilder.Entity<userInfo>()
                .Property(e => e.mail)
                .IsUnicode(false);

            modelBuilder.Entity<userInfo>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<userInfo>()
                .Property(e => e.activeStatus)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<userInfo>()
                .HasMany(e => e.equipements)
                .WithOptional(e => e.userInfo)
                .HasForeignKey(e => e.idOwner);

            modelBuilder.Entity<userInfo>()
                .HasMany(e => e.items)
                .WithOptional(e => e.userInfo)
                .HasForeignKey(e => e.idOwner);
        }
    }
}
