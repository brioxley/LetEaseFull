import { UserType, UserRole } from './user-enums';

export interface RegisterUserDto {
  email: string;
  password: string;
  username: string;
  firstName?: string;
  lastName?: string;
  type?: UserType;
  role?: UserRole;
  companyId?: number;
}



