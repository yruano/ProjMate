public class Project
{
    public long Id { get; set; }
    public required string Projectname { get; set; }
    public required string Username { get; set; } // 인증된 사용자명 자동 입력
    public required string MaxMember { get; set; }
    public required string Category { get; set; }
    public required string Text { get; set; }
}
