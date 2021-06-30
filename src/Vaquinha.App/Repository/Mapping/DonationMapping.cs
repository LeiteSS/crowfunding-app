using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vaquinha.App.Entities;

namespace Vaquinha.App.Repository.Mapping
{
    public class DonationMapping : IEntityTypeConfiguration<Donation>
    {
        public void Configure(EntityTypeBuilder<Donation> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(e => e.Value)
                .IsRequired()
                .HasColumnType("decimal(9,2)");

            builder.Property(e => e.DateAndTime)
                .IsRequired();

            builder.HasOne(d => d.PersonalData)
                .WithMany(p => p.Donations)
                .HasForeignKey(d => d.PersonalDataId);

            builder.HasOne(d => d.BillingAddress)
                .WithMany(e => e.Donations)
                .HasForeignKey(d => d.BillingAddressId);

            // nao salva os dados de cartao na base de dados
            builder.Ignore(e => e.FormOfPayment);
            builder.Ignore(e => e.ValidationResult);
            builder.Ignore(e => e.ErrorMessages);

            builder.ToTable("Donations");
        }
    }
}