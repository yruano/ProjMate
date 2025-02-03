public class User
{
    public Int64 Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; } // 실제로는 해시된 비밀번호 저장
    public required string Salt { get; set; }     // 솔트
    public required string DiscordId { get; set; }
    public required string Position { get; set; }
}
