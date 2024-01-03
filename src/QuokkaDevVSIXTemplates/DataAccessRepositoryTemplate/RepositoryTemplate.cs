using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace $rootnamespace$
{
    internal class $safeitemname$ : BaseRepository<$fileinputname$>, I$safeitemname$
    {
        public $safeitemname$(CommandsDbContext context) : base(context)
        {
        }
    }
}
