using ErrorOr;

namespace Divtos.Domain.Commons.Errors;

public partial class Errors
{
    public static class Group
    {
        public static Error NotFound(int idGroup) => Error.NotFound("Group.NotGroup",$"Group with id {idGroup} not found");
        
    }
    
}