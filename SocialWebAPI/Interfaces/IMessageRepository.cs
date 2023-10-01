
using SocialWebAPI.Helpers;

namespace SocialWebAPI;

public interface IMessageRepository
{
    void AddMessage(Message message);
    void DeleteMessage(Message message);
    Task<Message> GetMessage(int id);
    Task<PageList<MessageDto>> GetMessageForUser();
    Task<IEnumerable<MessageDto>> GetMessageThread(int currentUserId, int recipientId);
    Task<bool> SaveAllAsync();
}
