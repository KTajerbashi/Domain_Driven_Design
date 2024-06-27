namespace Extensions.MessageBus.Abstractions;
/// <summary>
/// جهت ارسال پیام از این زیر ساخت اتستفاده می‌شود
/// پیام ها می‌توانند در قالب دستوری باشند که به سرویسی خاص ارسال می‌شوند یا پیامی عمومی که برای همه ارسال می‌شود
/// </summary>
public interface ISendMessageBus
{
    /// <summary>
    /// پیامی که ان تا گیرنده میتواند داشته باشد پابلیش میشود
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <param name="input"></param>
    void Publish<TInput>(TInput input);

    /// <summary>
    /// برای گیرنده خاصی ارسال میکنیم
    /// </summary>
    /// <typeparam name="TCommandData"></typeparam>
    /// <param name="destinationService"></param>
    /// <param name="commandName"></param>
    /// <param name="commandData"></param>
    void SendCommandTo<TCommandData>(string destinationService, string commandName, TCommandData commandData);
   
    /// <summary>
    /// ارسال پیام برای شی وابسته
    /// </summary>
    /// <typeparam name="TCommandData"></typeparam>
    /// <param name="destinationService"></param>
    /// <param name="commandName"></param>
    /// <param name="correlationId"></param>
    /// <param name="commandData"></param>
    void SendCommandTo<TCommandData>(string destinationService, string commandName, string correlationId, TCommandData commandData);
   
    /// <summary>
    /// ارسال عادی 
    /// </summary>
    /// <param name="parcel"></param>
    void Send(Parcel parcel);
}
