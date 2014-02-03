type ServiceInterface<'T, 'U> = 
    abstract Actor : MailboxProcessor<'T> with get
    abstract Event : IEvent<'U> with get

type TimerCommand =
    | Tick of double

and TimerView =
    abstract SetTime: double -> unit