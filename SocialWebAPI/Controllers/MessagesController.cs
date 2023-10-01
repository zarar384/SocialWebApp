using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialWebAPI.Controllers;
using SocialWebAPI.Interfaces;

namespace SocialWebAPI;

public class MessagesController : BaseAPIController
{
    private readonly IMapper _mapper;
    private readonly IMessageRepository _messageRepository;
    private readonly IUserRepository _userRepository;
    public MessagesController(IUserRepository userRepository, IMessageRepository messageRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _messageRepository = messageRepository;
        _mapper = mapper;

    }

    public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
    {
        var username = User.GetUsername();

        if (username == createMessageDto.RecipientUsername.ToLower())
            return BadRequest("You cannot send messages to yourself");

        var sender = await _userRepository.GetByUserNameAsync(username);
        var recipient = await _userRepository.GetByUserNameAsync(createMessageDto.RecipientUsername);

        if (recipient == null) return NotFound();

        var message = new Message
        {
            Sender = sender,
            Recipient = recipient,
            SenderUsername = sender.UserName,
            RecipientUsername = recipient.UserName,
            Content = createMessageDto.Content
        };

        _messageRepository.AddMessage(message);

        if (await _messageRepository.SaveAllAsync()) return Ok(_mapper.Map<MessageDto>(message));

        return BadRequest("Failed to send message");
    }
}
