using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace HospitalManager
{
    internal class DataProvider
    {

        internal class RegistrationPage
        {
            internal static string DocListQuery() { return "select role_id, description from tobehospital.roles where (role_id like 'DC%')"; }
            internal static string StfListQuery() { return "select role_id, description from tobehospital.roles where (role_id like 'ST%')"; }
            internal static string ExistQuery(string email) { return "select email from tobehospital.users where email='" + email + "'"; }
            internal static string LastIdQuery(string _role) 
            { return "select * from (select user_id from tobehospital.users where user_id like '" + _role + "%' order by to_number(create_id) desc) where rownum=1"; }
        }

        internal class LoginPage
        {
            internal static string PasswordQuery(string userId)
            { return "select password,user_id,first_name,status from tobehospital.users where first_name='" + userId + "' or email='" + userId + "' or user_id='" + userId + "'"; }
        }

        internal class LoggedInPage
        {
            internal static string getPictureAvailability(string userId) { return "select user_id from tobehospital.Images where user_Id='" + userId + "'"; }
            internal static string getPicAddress(string userId) 
            { return "select img_adr from tobehospital.images where img_adr=(select img_adr from tobehospital.images where user_id='" + userId + "')"; }
            internal static string getHistoryInfo(string userId)
            {
                return " select * from (select b.description, a.date_upd, a.updr_id from tobehospital.audit_trail a left join tobehospital.upd_codes b " +
                "on a.upd_code=b.upd_code where user_id='" + userId + "' or updr_id='" +userId +"' order by a.date_upd desc) where rownum <= 20";
            }
            internal static string getGeneralInfo(string userIdSubString)
            {
                return "select * from (select a.topic,a.info,a.user_id,b.date_upd from tobehospital.General_info a inner join "+
                    "tobehospital.audit_trail b on a.trans_id=b.trans_id where (recipient_code='ALL' or recipient_code like '%" + userIdSubString + 
                  "%') and a.del_flag='0' order by to_number(a.trans_id) desc) where rownum<=20";
            }
        }

        internal class UpdateRegistration
        {
            internal static string getInfo(string tableName, string userId) { return "select * from tobehospital." + tableName + " where user_Id='" + userId + "'"; }
        }

        internal class RegAdminSup
        {
            internal static string ExistQuery(string email) { return "select email from tobehospital.users where email='" + email + "'"; }
            internal static string LastIdQuery(string _role)
            { return "select * from (select user_id from tobehospital.users where user_id like '" + _role + "%' order by to_number(create_id) desc) where rownum=1"; }
        }

        internal class DeptWardPage
        {
            internal static string verifyDeptAdd(string dept) { return "select dept_name from tobehospital.departments where dept_name='" + dept + "'"; }
            internal static string fillDeptList() { return "select dept_name,description from tobehospital.departments where no_of_wards='0'"; }
            internal static string verifyWardAdd(string ward) { return "select ward_name from tobehospital.wards where ward_name='" + ward + "'"; }
            internal static string fillWardList() { return "select ward_name from tobehospital.wards where no_of_rooms='0' and status='unrelated'"; }
        }

        internal class AddInformationPage
        {
            internal static string checkTopic(string topic) { return "select topic from tobehospital.general_info where topic='" + topic + "' and del_flag='0'"; }
        }

        internal class DeleteInformationPage
        {
            internal static string getGeneralInfo(string orderBy="to_number(trans_id) desc")
            {
                return "select b.description, a.user_id, a.topic, a.info from tobehospital.general_info a left join " +
                  "tobehospital.other_codes b on a.recipient_code=b.code where a.del_flag='0' order by " + orderBy;
            }
            internal static string checkTopic(string topic) { return "select topic from tobehospital.general_info where topic='" + topic + "' and del_flag='0'"; }
        }

        internal class ChangePassword
        {
            internal static string getPassword(string userId) { return "select password from tobehospital.users where user_id='" + userId + "'"; }
            internal static string getEmail(string userId) { return "select email from tobehospital.users where user_id='" + userId + "'"; }
            internal static string getEmailPassword(string email) { return "select first_name,user_id,password from tobehospital.users where email='" + email + "'"; }
        }

        internal class Departments
        {
            internal static string deptIdName(string deptId, string deptName)
            { return "select dept_id,dept_name from tobehospital.departments where (dept_id='" + deptId + "' or dept_name='" + deptName + "') and del_flag='0'"; }
            internal static string fillListView(string orderBy="to_number(trans_id) desc")
            { return "select dept_name,description,dept_id,no_of_wards from tobehospital.departments where del_flag='0' order by " + orderBy; }
        }

        internal class Wards
        {
            internal static string roomDeptAvail(string wardName) { return "select no_of_rooms from tobehospital.wards where ward_name='" + wardName + "'"; }
            internal static string wardIdName(string wardId, string wardName)
            { return "select ward_id,ward_name from tobehospital.wards where (ward_id='" + wardId + "' or ward_name='" + wardName + "') and del_flag='0'"; }
            internal static string fillListView(string orderBy = "LPAD(ward_name,64)") 
            { return "select ward_name,description,ward_id,parent_dept from tobehospital.wards where del_flag='0' order by " + orderBy; }
        }

        internal class RelDeptWardPage
        {
            internal static string getDepartments() { return "select dept_name from tobehospital.departments where no_of_wards='0' and del_flag='0' order by dept_name asc"; }
            internal static string getDeptInfo(string deptName)
            { return "select dept_name, dept_id, description from tobehospital.departments where dept_name='" + deptName + "'"; }
            internal static string getWards() { return "select * from (select ward_name from tobehospital.wards where parent_dept='None' and del_flag='0' minus "+
                "select ward_name from tobehospital.ward_temp) order by LPAD(ward_name,64)";
            }
            internal static string getWardInfo(string wardName)
            { return "select ward_name, ward_id, description from tobehospital.wards where ward_name='" + wardName + "'"; }
            internal static string addWardsToListView(){ return "select ward_id, ward_name, description from tobehospital.ward_temp order by ward_name asc"; }
        }

        internal class DiscDeptWard
        {
            internal static string getDepartments() { return "select dept_name from tobehospital.departments where no_of_wards!='0' order by dept_name asc"; }
            internal static string getDeptInfo(string deptName)
            { return "select dept_name, dept_id, description from tobehospital.departments where dept_name='" + deptName + "'"; }
            internal static string addWardsToListView(string deptName)
            { return "select ward_id, ward_name, description from tobehospital.wards where parent_dept='" + deptName + "' order by LPAD(ward_name,64)"; }
        }

        internal class Unsubscribe
        {
            internal static string getPicAddress(string userId) 
            { return "select img_adr from tobehospital.images where img_adr=(select img_adr from tobehospital.images where user_id='" + userId + "')"; }
        }

        internal class Patients
        {
            internal static string fillListView(string userId = "%%")
            {
                return "select create_id,user_id,organ,description,status from tobehospital.pat_disab where user_id like '" +
                  userId + "' and del_flag='0' order by user_id, organ";
            }
            internal static string getName(string userId) 
            { return "select last_name, first_name, other_name from tobehospital.patients where user_id='" + userId + "'"; }
        }

        internal class DoctorStaffHistory
        {
            internal static string fillListViewDoc(string userId = "%%")
            {
                return "select create_id,user_id,wk_plc,wk_typ,pos,to_char(date_str,'dd/mm/yyyy') as start_date,to_char(date_end,'dd/mm/yyyy') as end_date from tobehospital.doc_wk_hist where user_id like '" +
                    userId + "' and del_flag='0' order by user_id, date_str";
            }
            internal static string fillListViewStf(string userId = "%%")
            {
                return "select create_id,user_id,wk_plc,wk_typ,pos,to_char(date_str,'dd/mm/yyyy') as start_date,to_char(date_end,'dd/mm/yyyy') as end_date from tobehospital.stf_wk_hist where user_id like '" +
                    userId + "' and del_flag='0' order by user_id, date_str";
            }
            internal static string getNameDoc(string userId)
            { return "select last_name, first_name, other_name from tobehospital.doctors where user_id='" + userId + "'"; }
            internal static string getNameStf(string userId)
            { return "select last_name, first_name, other_name from tobehospital.staffs where user_id='" + userId + "'"; }
            internal static string checkExistDoc(string userId, string workPlace, string position)
            {
                return "select user_id from tobehospital.doc_wk_hist where user_id='" + userId + "' and wk_plc='" + workPlace + "' and pos='" +
                    position + "' and del_flag='0'";
            }
            internal static string checkExistStf(string userId, string workPlace, string position)
            {
                return "select user_id from tobehospital.stf_wk_hist where user_id='" + userId + "' and wk_plc='" + workPlace + "' and pos='" +
                    position + "' and del_flag='0'";
            }
        }

        internal class GeneralClass
        {
            internal static string getLastTransactionId()
            { return "select * from (select trans_id from tobehospital.audit_trail order by date_upd desc) where rownum=1"; }
            internal static string getDoctorsInfo()
            {
                return " select first_name,last_name,phone_no,email,other_info,img_adr from tobehospital.doctors a left join tobehospital.images b" +
                    " on a.user_id=b.user_id where a.reg_status='Validated' order by upper(last_name)";
            }
        }
    }
}