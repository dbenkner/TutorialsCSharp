using CompareHash;

Hasher.CompHash();

Console.WriteLine(Hasher.ComputeSha256Hash("Google"));
Console.WriteLine(Hasher.ComputeSha256Hash("Google", 1));
Console.WriteLine(Hasher.ComputeSha256Hash("Google", 2));
Console.WriteLine(Hasher.ComputeSha256Hash("Google", 3));

Console.WriteLine(Hasher.ComputeSha512Hash("Google"));

Console.WriteLine(Hasher.SHA512short("Google.com"));

Console.WriteLine(RandomStringGenerator.CreateRandom("Google.com", 8));