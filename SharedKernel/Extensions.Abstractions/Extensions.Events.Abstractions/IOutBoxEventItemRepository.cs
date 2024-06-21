namespace Extensions.Events.Abstractions
{
    public interface IOutBoxEventItemRepository
    {
        public List<OutBoxEventItem> GetOutBoxEventItemsForPublisher(int maxCount = 100);
        void MarkAsRead(List<OutBoxEventItem> outBoxEventItems);
    }

}
