import React,{useEffect} from 'react';
import { DetailsList,Link } from '@fluentui/react';
import { mergeStyleSets } from '@fluentui/react/lib/Styling';
import {useQuery} from "react-query";
import { FetchAPI } from '../Services/APIRequest'

import 'office-ui-fabric-react/dist/css/fabric.css';
import { GetSurveyListEP } from '../lib/endpoints';
import { CommandBarButton, IContextualMenuProps, IIconProps, Stack, IStackStyles,Checkbox } from '@fluentui/react';

import { useHistory } from "react-router-dom";

export interface IButtonExampleProps {
  // These are set based on the toggles shown above the examples (not needed in real code)
  disabled?: boolean;
  checked?: boolean;
}

const addIcon: IIconProps = { iconName: 'Add' };
const stackStyles: Partial<IStackStyles> = { root: { height: 44 } };


const classNames = mergeStyleSets({
    grid: {
      margin: 'auto',
    }
  });
const columns = [
    { key: 'ColumnFullName', name: 'Full Name', fieldName: 'fullName', minWidth: 100, maxWidth: 200, isResizable: true },
    { key: 'columnGender', name: 'Gender', fieldName: 'gender', minWidth: 100, maxWidth: 90, isResizable: true },
    { key: 'columnBirthdate', name: 'Birth Date', fieldName: 'birthdate', minWidth: 100, maxWidth: 90, isResizable: true },
    { key: 'columnNumberOfKids', name: 'Number Of Kids', fieldName: 'numberOfKids', minWidth: 90, maxWidth: 200, isResizable: true },
    { key: 'columnNumberOfKids', name: '', fieldName: '', minWidth: 90, maxWidth: 200, isResizable: true },
    
  ]
  const operations = [
    {
      from: '0000 0284 7529 4751 8475',
      to: '0980 9808 8200 7680',
      amount: '$1 510',
      date: '20-05-2020',
    },
]

const getSurveyObjectlist = async ({  }) => {
   debugger;
  const response = await FetchAPI(GetSurveyListEP);

  return response;
};
  

 
 const GridTable = () => {

  const { isLoading, isError, data, error, refetch } = useQuery(["GetSurveylist"],
  getSurveyObjectlist,
  {
    enabled: true,
    refetchOnWindowFocus: true,
  },
);

const history=useHistory();
function NewItem (){
  debugger;
  // return <Redirect to='/StrategicPlanningforworkforce'  />
  // let history = useHistory();
  history.push('/Survey')

  
}

    return (
     
        <div data-is-scrollable={true}>
          
            <div className={`${"s - Grid - col ms-sm9 ms-xl9"}${classNames.grid}`}>
            <Stack horizontal styles={stackStyles}>
            
      <CommandBarButton 
        iconProps={addIcon}
        text="New item"
        onClick={NewItem}
      />
    </Stack>
            {data && data.length>0?
            <DetailsList
                items={data}
                columns={columns}
                selectionMode={0} 
                onRenderItemColumn={(item: any, index: number|undefined,column:any) => {
                  if(column)
                  if (column.fieldName === '') {
                    return <Link data-selection-invoke={true}>{"Edit"}</Link>;
                }
              
                return item[column.fieldName];
                 }}
                >
                
             </DetailsList>
                :
                'No data to shown.'}
            
        </div>
    </div >
  );
};

export default GridTable;