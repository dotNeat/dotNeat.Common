namespace dotNeat.Common.DataAccess.Repository
{
    public interface IReversableRepository
    {
        bool CommitAddedEntities();
        bool CommitModifiedEntities();
        bool CommitDeletedEntities();
        bool CommitAllChanges();

        bool ReverseAddedEntities();
        bool ReverseModifiedEntities();
        bool ReverseDeletedEntities();
        bool ReverseAllChanges();
    }
}
