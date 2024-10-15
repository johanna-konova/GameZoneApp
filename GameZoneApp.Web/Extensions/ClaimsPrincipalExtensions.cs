namespace System.Security.Claims
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid Id(this ClaimsPrincipal user)
            => Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }
}
