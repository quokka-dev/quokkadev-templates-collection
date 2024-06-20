using System.Runtime.Remoting;

namespace $rootnamespace$
{
    public static class $fileinputname$Factory
    {
        public static $fileinputname$ CreateNew$fileinputname$()
        {
            $fileinputname$ newAggregate = new $fileinputname$();

            newAggregate.AddDomainEvent(new $fileinputname$CreatedEvent(newAggregate));

            return newAggregate;
        }
    }
}
