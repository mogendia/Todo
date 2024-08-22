using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo_Api.Models;

namespace Todo_Api.Configuration
{
    public class MissionEntityType : IEntityTypeConfiguration<TodoTask>
    {
        public void Configure(EntityTypeBuilder<TodoTask> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.IsCompleted).IsRequired();
            builder.HasData(new TodoTask { Id = 1, Name = "Mo", IsCompleted = true });
        }
    }
}
