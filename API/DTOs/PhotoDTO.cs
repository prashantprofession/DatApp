﻿namespace API.DTOs;

public class PhotoDTO
{
    public int Id { get; set; }
    public string Url { get; set; }
    public bool isMain { get; set; }
    public string PublicId { get; set; }
}
