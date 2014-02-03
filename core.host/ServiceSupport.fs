namespace com.example

#nowarn "40"

open System

type ServiceInterface<'T, 'U> = 
    abstract Actor : MailboxProcessor<'T> with get
    abstract Event : IEvent<'U> with get

type TimerCommand =
    | SetTimerView of ITimerView
    | Tick
    | ResetTimer

and ITimerView =
    abstract SetTicks: int -> unit