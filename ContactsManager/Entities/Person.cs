﻿namespace Entities;

public class Person
{
  public Guid Id { get; set; }
  public string? Name { get; set; }
  public string? Email { get; set; }
  public DateTime? DateOfBirth { get; set; }
  public string? Gender { get; set; }
  public string? Address { get; set; }
  public bool ReceiveNewsLetters { get; set; }

  public Guid? CountryId { get; set; }
}