using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1_1105_TSET_member5.Models;

public partial class Customer
{
    public string CustomerId { get; set; } = null!;

    public int Id { get; set; }

    public string UserId { get; set; } = null!;

	[StringLength(12, ErrorMessage = "密碼長度至少為 {2} 個字符", MinimumLength = 8)]
	[RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)[A-Za-z\d]{8,}$",
		ErrorMessage = "密碼必須包含大小寫、數字")]
	public string? Password { get; set; }

	[EmailAddress(ErrorMessage = "請輸入有效的電子郵件地址")]
	public string? Email { get; set; }

	[RegularExpression(@"^09\d{8}$", ErrorMessage = "必須是 09 開頭，並且後面跟著 8 位數字。")]
	public string? Phone { get; set; }

    public string? Name { get; set; }

    public bool? Gender { get; set; }

    public DateOnly? Birthday { get; set; }

    public string? ImgSrc { get; set; }

    public string? Language { get; set; }

    public string? ReceiverAddress { get; set; }

    public string? CreateUser { get; set; }

    public string? UpdateUser { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }
}
