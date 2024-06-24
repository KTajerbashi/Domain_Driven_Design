namespace Extensions.Events.Abstractions
{
    public interface IOutBoxEventItemRepository
    {
        public List<OutBoxEventItem> GetOutBoxEventItemsForPublish(int maxCount = 100);
        void MarkAsRead(List<OutBoxEventItem> outBoxEventItems);
    }

}
