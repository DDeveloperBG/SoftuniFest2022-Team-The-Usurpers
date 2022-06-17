namespace App.Services.Data.BaseModel
{
    using System;

    using App.Data.Common.Models;

    public interface IBaseModelService
    {
        DateTime GetLastCreatedTime<T>()
            where T : BaseModel<string>;
    }
}
