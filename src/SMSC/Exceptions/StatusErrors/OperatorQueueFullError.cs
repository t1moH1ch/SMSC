﻿namespace SMSC.Exceptions.StatusErrors;

public class OperatorQueueFullError : ISmsError
{
    public int Code => 220;
    public string Name => "Переполнена очередь у оператора";
    public string Description => "Указанная ошибка может возникать в случае, когда абонент недоступен " +
        "для приема SMS, но сообщения продолжают приходить оператору и происходит переполнение внутренней очереди " +
        "сообщений для данного абонента. В редких случаях возможно появление ошибки в результате сбоя в сети самого " +
        "оператора или переполнении общей очереди сообщений. Во всех подобных ситуациях система с определенными " +
        "интервалами несколько раз пытается отправить указанные сообщения повторно.";
}
