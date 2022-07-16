# PROJECT WEBSITE PROGRAMING - ASP.NET MVC 

Name of the project: *Website xem phim Twoone*

## Abstract
- Website watching movie Twoone with up to **4000 films** and many interesting functions. We use asp.net MVC 5 technology to build the website. 
  The website has some layouts look like this:

- Here is short demo about Twoone website. [Click here to see full!!!](aa)

  <video src="C:\Users\vothu\Downloads\WebXemPhim_Twoone\source\short demo.mp4"></video>
## Idea

### Objects use

- User: watch movie, comment, update premium edit profile, register, login, forgot password, search.
- Admin: 
    - Management: film, director, actor, hashtags, genres, licenses.
    - Statistic: revenue month, revenue year, user subscribe month, user subscribe year, number of subscribers.

### Entity relationship diagram(ERD)

<img src="C:\Users\vothu\Downloads\WebXemPhim_Twoone\source\ERD.png" style="zoom:60%;" alt ="ERD"/>

### Use case summary

<img src="C:\Users\vothu\Downloads\WebXemPhim_Twoone\source\Usecasetq.png" style="zoom:60%" alt="usecase" />

## Requirements
- C# 
- Entity Framework (code first)
- Asp.net MCV 5
- Other library: pagelist, md5, mail, jquery, bootstrap
## Database (MSSQL)

### Diagram

<img src="C:\Users\vothu\Downloads\WebXemPhim_Twoone\source\sql.png" style="zoom:60%;" alt="sql diagram"/>

You can see more at full report in source folder or 

[**Click here!!!**]: ~/Source/fullreport.pdf	"Click here!!!"



#### Interface and Function of website

##### Home page

##### Login & Register & Reset Password

| Login                                                        | Register                                                     | Reset password                                               |
| ------------------------------------------------------------ | ------------------------------------------------------------ | ------------------------------------------------------------ |
| ![login](C:\Users\vothu\Downloads\WebXemPhim_Twoone\source\login.png) | ![register](C:\Users\vothu\Downloads\WebXemPhim_Twoone\source\register.png) | ![twoonez-001-site1.itempurl.com_User_ForgotPassword_class=small](C:\Users\vothu\Downloads\WebXemPhim_Twoone\source\twoonez-001-site1.itempurl.com_User_ForgotPassword_class=small.png) |

##### List film & actor

| List film - type list                                        | List film - type grid                                        | List actor                                                   |
| ------------------------------------------------------------ | ------------------------------------------------------------ | ------------------------------------------------------------ |
| <img src="C:\Users\vothu\Downloads\WebXemPhim_Twoone\source\twoonez-001-site1.itempurl.com_Movie_SearchByFilter_Grid=False.png" alt="twoonez-001-site1.itempurl.com_Movie_SearchByFilter_Grid=False" style="zoom:50%;" /> | <img src="C:\Users\vothu\Downloads\WebXemPhim_Twoone\source\twoonez-001-site1.itempurl.com_Movie_SearchByFilter.png" alt="twoonez-001-site1.itempurl.com_Movie_SearchByFilter"  /> | <img src="C:\Users\vothu\Downloads\WebXemPhim_Twoone\source\twoonez-001-site1.itempurl.com_Movie_ActorGrid.png" alt="twoonez-001-site1.itempurl.com_Movie_ActorGrid"  /> |

#### Movie Details

| film information                                             | similar film                                                 |
| ------------------------------------------------------------ | ------------------------------------------------------------ |
| ![twoonez-001-site1.itempurl.com_Movie_MovieDetail_3262](C:\Users\vothu\Downloads\WebXemPhim_Twoone\source\twoonez-001-site1.itempurl.com_Movie_MovieDetail_3262.png) | ![twoonez-001-site1.itempurl.com_Movie_MovieDetail_3262 (2)](C:\Users\vothu\Downloads\WebXemPhim_Twoone\source\twoonez-001-site1.itempurl.com_Movie_MovieDetail_3262 (2).png) |

##### Watching movie

##### <img src="C:\Users\vothu\Downloads\WebXemPhim_Twoone\source\twoonez-001-site1.itempurl.com_Movie_WatchingMovie_3262.png" alt="twoonez-001-site1.itempurl.com_Movie_WatchingMovie_3262" style="zoom: 50%;" />

#### Prerium

| Type of service                                              | type of payment                                              |
| ------------------------------------------------------------ | ------------------------------------------------------------ |
| ![twoonez-001-site1.itempurl.com_Payment_PaymentPage](C:\Users\vothu\Downloads\WebXemPhim_Twoone\source\twoonez-001-site1.itempurl.com_Payment_PaymentPage.png) | ![twoonez-001-site1.itempurl.com_Payment_ChonPhuongThucThanhToan_1](C:\Users\vothu\Downloads\WebXemPhim_Twoone\source\twoonez-001-site1.itempurl.com_Payment_ChonPhuongThucThanhToan_1.png) |

##### About us & Page not found

| About us                                                     | 404 page                                                     |
| ------------------------------------------------------------ | ------------------------------------------------------------ |
| <img src="C:\Users\vothu\Downloads\WebXemPhim_Twoone\source\about us.png" alt="about us" style="zoom:40%;" /> | <img src="C:\Users\vothu\Downloads\WebXemPhim_Twoone\source\twoonez-001-site1.itempurl.com_Movie_PageNotFound_aspxerrorpath=_Movie_ActorList.png" alt="twoonez-001-site1.itempurl.com_Movie_PageNotFound_aspxerrorpath=_Movie_ActorList" style="zoom:50%;" /> |

#### User

| uSER PROFILE                                                 | FILM RATED                                                   | CHANGE PASS                                                  |
| ------------------------------------------------------------ | ------------------------------------------------------------ | ------------------------------------------------------------ |
| ![twoonez-001-site1.itempurl.com_User_ShowInfor](C:\Users\vothu\Downloads\WebXemPhim_Twoone\source\twoonez-001-site1.itempurl.com_User_ShowInfor.png) | ![twoonez-001-site1.itempurl.com_User_ShowInfor_mode=favorite](C:\Users\vothu\Downloads\WebXemPhim_Twoone\source\twoonez-001-site1.itempurl.com_User_ShowInfor_mode=favorite.png) | ![twoonez-001-site1.itempurl.com_User_ShowInfor_mode=changepass](C:\Users\vothu\Downloads\WebXemPhim_Twoone\source\twoonez-001-site1.itempurl.com_User_ShowInfor_mode=changepass.png) |

##### Admin

| Login                                                        | Forget password                                              |
| ------------------------------------------------------------ | ------------------------------------------------------------ |
| ![twoonez-001-site1.itempurl.com_Administrator_Adm_TrangChu_Login](C:\Users\vothu\Downloads\WebXemPhim_Twoone\source\twoonez-001-site1.itempurl.com_Administrator_Adm_TrangChu_Login.png) | ![twoonez-001-site1.itempurl.com_Administrator_Adm_TrangChu_ForgotPassword](C:\Users\vothu\Downloads\WebXemPhim_Twoone\source\twoonez-001-site1.itempurl.com_Administrator_Adm_TrangChu_ForgotPassword.png) |

##### Home Page Admin

![twoonez-001-site1.itempurl.com_Administrator_Adm_TrangChu_Adm_Home](C:\Users\vothu\Downloads\WebXemPhim_Twoone\source\twoonez-001-site1.itempurl.com_Administrator_Adm_TrangChu_Adm_Home.png)

##### 8 function management (C-R-U-D)

example: Film Management

| List film                                                    | Delete                                                       | Create                                                       | Edit                                                         |
| ------------------------------------------------------------ | ------------------------------------------------------------ | ------------------------------------------------------------ | ------------------------------------------------------------ |
| ![twoonez-001-site1.itempurl.com_Administrator_Adm_Phim](C:\Users\vothu\Downloads\WebXemPhim_Twoone\source\twoonez-001-site1.itempurl.com_Administrator_Adm_Phim.png) | ![twoonez-001-site1.itempurl.com_Administrator_Adm_Phim_Delete_3262](C:\Users\vothu\Downloads\WebXemPhim_Twoone\source\twoonez-001-site1.itempurl.com_Administrator_Adm_Phim_Delete_3262.png) | ![twoonez-001-site1.itempurl.com_Administrator_Adm_Phim_Create](C:\Users\vothu\Downloads\WebXemPhim_Twoone\source\twoonez-001-site1.itempurl.com_Administrator_Adm_Phim_Create.png) | ![twoonez-001-site1.itempurl.com_Administrator_Adm_Phim_Edit_3262](C:\Users\vothu\Downloads\WebXemPhim_Twoone\source\twoonez-001-site1.itempurl.com_Administrator_Adm_Phim_Edit_3262.png) |

#### Statical

<img src="C:\Users\vothu\Downloads\WebXemPhim_Twoone\source\twoonez-001-site1.itempurl.com_Administrator_Adm_TrangChu_Statistical.png" alt="twoonez-001-site1.itempurl.com_Administrator_Adm_TrangChu_Statistical" style="zoom:50%;" />

Above is all of function we made

Any question you can contact with us

email: vothuongtruongnhon2002@gmail.com

Author:

| leader                | member       | member         |
| --------------------- | ------------ | -------------- |
| Võ Thương Trường Nhơn | Phạm Đức Tài | Phạm Hồng Thái |

