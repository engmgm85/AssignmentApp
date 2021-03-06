import React from 'react';
import { Nav, initializeIcons } from '@fluentui/react';
const navigationStyles = {
    root: {
      height: '100vh',
      boxSizing: 'border-box',
      border: '1px solid #eee',
      overflowY: 'auto',
      paddingTop: '10vh',
    },
  };
const links = [
    {
        links: [
            {
        name: 'Home',
        key:'key1',
        url: '/',
        iconProps: {
          iconName: 'Home',
          styles: {
            root: {
              fontSize: 20,
              color: '#106ebe',
            },
          }
    }
},
]
    }
];

const Navigation = () => {
    initializeIcons();
    return (
      <Nav
        groups={links}
        selectedKey='key1'
        styles={navigationStyles}
      />
    );
  };
  
  export default Navigation;