import { UserType, UserRole } from './user-enums';

export interface RegisterUserDto {
  Username: string;
  Email: string;
  Password: string;
  FirstName: string;
  LastName: string;
  Type: UserType;
  Role: UserRole;
  CompanyId?: number;
}



