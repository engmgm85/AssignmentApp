import * as React from "react";
import { useForm, Controller, FormProvider } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import cogoToast from "cogo-toast";
import { CommonFormProps } from "../lib/commontypes";
import { useQuery } from "react-query";
import { useHistory } from "react-router-dom";
import Message from "./Message";
import { TextField, MaskedTextField,ITextFieldStyles } from '@fluentui/react/lib/TextField';
import {
    DatePicker,
    IDatePickerStrings,
    defaultDatePickerStrings,
    addMonths,
    addYears,
    IDatePickerStyles,
    addDays,  Toggle,Checkbox
  } from '@fluentui/react';

  import { useConst } from '@fluentui/react-hooks';
  import { useBoolean } from '@fluentui/react-hooks';
import { useState } from "react";
import { FetchAPI } from '../Services/APIRequest'
import { GetSurveyListEP,HearSourceListEP } from '../lib/endpoints';
type Querykey = 
{
    queryKey:any
}
const textFieldStyles: Partial<ITextFieldStyles> = { fieldGroup: { width: 300 } };
const narrowTextFieldStyles: Partial<ITextFieldStyles> = { fieldGroup: { width: 100 } };
const stackTokens = { childrenGap: 15 };
const datePickerStyles: Partial<IDatePickerStyles> = { root: { maxWidth: 300, marginTop: 15 } };

const getHearAboutUs = async ({  }) => {
    debugger;
   const response = await FetchAPI(HearSourceListEP);
 
   return response;
 };
  
 
 const GETSurveyByid = async ({  }) => {
    debugger;
   const response = await FetchAPI(GetSurveyListEP);
 
   return response;
 };
   

const SurveyForm: React.FC<CommonFormProps> = ({
    rid,
    
 
  }: CommonFormProps) => {
    const today = useConst(new Date(Date.now()));
    const minDate = useConst(addYears(today, -100));
    const maxDate = useConst(today);
  
    const DateTimeLimitMessages: IDatePickerStrings = useConst(() => ({
      ...defaultDatePickerStrings,
      isOutOfBoundsErrorMessage: `Date must be between ${minDate.toLocaleDateString()} and ${maxDate.toLocaleDateString()}`,
    }));
    const history=useHistory();
    const { isLoading, isError, data, error } = useQuery(
        ["getHearAboutUs", { rid }],
        getHearAboutUs,
        {
          enabled: true,
          //process.env.NEXT_PUBLIC_RID_NEW,
          refetchOnWindowFocus: false,
        },
      );
    // const { isLoading, isError, data, error } = useQuery(
    //     ["GETSurveyByid", { rid }],
    //     GETSurveyByid,
    //     {
    //       enabled: rid !== "" ,
    //       //process.env.NEXT_PUBLIC_RID_NEW,
    //       refetchOnWindowFocus: false,
    //     },
    //   );
      type surveyResourcesDTO = 
      {
        surveyId:number;
        hearSourceId:number;
      }
      type SurveyDTO =
      {
          rid: string;
          FullName:string;
          BirthDate:Date | string;
          Gender:boolean | string;
         NumberofKids :string;
         HearAboutus:Array<surveyResourcesDTO>
        
      } ;
      const Surveyschema = yup.object().shape({
        FullName:yup.string().required("Full Name is mandatory").min(10,"Minum length for full name is 10").max(50,"Maxium Length for full name is 50")
    });

    const Surveymethods = useForm<SurveyDTO>({
        mode: "onChange",
        resolver: yupResolver(Surveyschema),
        defaultValues: {
            FullName:"",
            BirthDate:"",
            Gender:"",
           NumberofKids :"",
        },
      });
      const {
        register,
        handleSubmit,
        formState,
        formState: { errors },
        setValue,
        setError,
        clearErrors,
        control,
        getValues,
        trigger,
        watch,
        reset,
      } = Surveymethods;

    
      const onSurveytSubmit = async (
          
        
        {
        rid:String,
        FullName,
        BirthDate,
        Gender,
       NumberofKids ,
       HearAboutus
      }) => {
        let payload: SurveyDTO = {
          rid,
        
          FullName,
          BirthDate:new Date(BirthDate).toISOString().slice(0, 10),
          Gender,
         NumberofKids ,
         HearAboutus
   
        };
    
      
    
        const {  ...saveobject } = payload;
    
        const response = await fetch("/api/saveExtendCreateObjectives", {
          method: "POST",
          headers: { "Content-Type": "application/json " },
          body: JSON.stringify(saveobject),
        });
    console.log(response);
        if (response.ok) {
          cogoToast.success(
            <Message
              title="Success"
              text="savedsuccessfully"
              type="success"
            />,
            {
              position: "bottom-center",
            },
          );
          return history.push("/");
        } else {
          return cogoToast.error(
            <Message title={"Error"} text="Something went wrong." type="error" />,
            {
              position: "bottom-center",
            },
          );
        }
      };

      React.useEffect(() => {
        if (
          data != undefined 
          &&
          data.data != undefined
        ) 
        {
            
         const Surveydefaultvalues = data.data;
          setValue("FullName", Surveydefaultvalues.fullName);
         
          setValue("BirthDate",new Date(Surveydefaultvalues.birthDate));
         
                     setValue("Gender",  Surveydefaultvalues.gender);
         
        
           setValue("NumberofKids",  Surveydefaultvalues.numberofKids);
         
          
        }
      }, [data]);
   
      const [FullNameTextValue, setFullNameTextValue] = React.useState('');
      const [FullNameerrorMessage,setFullNameerrorMessage]=React.useState('');
      const [Gender, { toggle: toggleGender }] = useBoolean(false);
      const [Genderlabel,setGenderlabel]=useState('Gender Male');
      const onChangeFullNameTextValue = React.useCallback(
        (event: React.FormEvent<HTMLInputElement | HTMLTextAreaElement>, newValue?: string) => {
          if (!newValue || newValue.length <= 50) {
            setFullNameTextValue(newValue || '');
          }
          else
          {
            setFullNameerrorMessage("Maxium allowed Length is 50");
          }
        },
        [],
      );
      const onChangeGender = React.useCallback(
        (event: any) => {
            toggleGender();
            Gender===true ? setGenderlabel('Gender FeMale'):setGenderlabel('Gender Male');
        },
        [],
      );

      function _onChange(ev?: React.FormEvent<HTMLElement | HTMLInputElement>, isChecked?: boolean) {
          debugger
          
        console.log(ev.currentTarget.id);
      }
      return (
        <React.Fragment>

            

           
        <form onSubmit={handleSubmit(onSurveytSubmit)}>
        <div className="ms-Grid m-8" dir="ltr">
        <div className="ms-Grid-row">
        <div className="ms-Grid-col ms-sm6 ms-md4 ms-lg4">

        <TextField
        label="Full Name"
        value={FullNameTextValue}
        onChange={onChangeFullNameTextValue}
        styles={textFieldStyles} required errorMessage={FullNameerrorMessage}
      />
       
        </div> </div>
        <div className="ms-Grid-row">
        <div className="ms-Grid-col ms-sm6 ms-md4 ms-lg4">
        <DatePicker label="Birth Date"  isRequired
        styles={datePickerStyles}
        strings={DateTimeLimitMessages}
        placeholder="Select a date..."
        ariaLabel="Birth Date"
        minDate={minDate}
        maxDate={maxDate}
        allowTextInput
      />
        </div>
        </div>
        <div className="ms-Grid-row">
        <div className="ms-Grid-col ms-sm6 ms-md4 ms-lg4">
   <Toggle label={Genderlabel} checked={Gender} onChange={onChangeGender} />
</div>
</div>

<div className="ms-Grid-row">
        <div className="ms-Grid-col ms-sm6 ms-md4 ms-lg4">

        <TextField
        label="Number Of Kids"
        value={FullNameTextValue}
        onChange={onChangeFullNameTextValue}
        styles={textFieldStyles} required errorMessage={FullNameerrorMessage}
      />
       
        </div> </div>
        <div className="ms-Grid-row">
        <div className="ms-Grid-col ms-sm6 ms-md4 ms-lg4">
    {data?data.map((item,i) =>(
 <Checkbox label={item.title} onChange={_onChange} id={item.id} className="py-2" />
    ))
    :null
}
  
        
    </div>
</div>
        </div>
          
          
     

                    
                      <button
                      
                     // onClick={(e) => router.push("/ExtendCreateObjectives")}
                      type="submit"
                     
                    >
                     Save
                    </button>
        </form>
      
            </React.Fragment>
     )
    }
    export default SurveyForm;