namespace APIntegro.Application.Services.Authentication;

public record ChallengeResponse
(
    bool Success,
    ChallengeResult Result
);

public record ChallengeResult
(
    string Token,
    string ServerTime,
    string ExpireTime
);
