﻿namespace SMSC.Exceptions.StatusErrors;

public class ConversionError : ISmsError
{
    public int Code => 241;
    public string Name => "Ошибка конвертации";
    public string Description => """
        При преобразовании текста или звукового файла в конечный формат для передачи абоненту голосового сообщения 
        произошла ошибка конвертации звука. Также данная ошибка может возникать при передаче SMS-сообщения отдельными 
        частями, когда оператору переданы не все части.
        """;
}