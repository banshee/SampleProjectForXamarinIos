namespace com.example

#nowarn "40"

open System

type ServiceInterface<'T, 'U> = 
    abstract Actor : MailboxProcessor<'T> with get
    abstract Event : IEvent<'U> with get

type TimerCommand =
    | SetTimerView of ITimerView
    | Tick of double

and ITimerView =
    abstract SetTime: double -> unit