import React from "react";
import classname from "classnames";


type Message =
{ title:String, text:String, type:String }
const Message = (pros:Message) => {
  
  return (
    <React.Fragment>
      <div
        className={classname(
          "fixed bottom-0              inset-x-0"
        
        )}
      >
        <div
          className={classname(
            { "bg-green-400": pros.type === "success" },
            { "bg-red-400": pros.type === "error" },
            { "bg-yellow-400": pros.type === "warning" },
            {
              "bg-indigo-700": pros.type === "default" || pros.type === "",
            },
          )}
        >
          <div className="max-w-screen-xl mx-auto py-4 px-3 sm:px-6 lg:px-8">
            <div className="flex  items-center justify-between flex-wrap ">
              <div className="w-0 flex-1 flex items-center">
                <span
                  className={classname(
                    "flex p-2 rounded-lg mx-4",
                    {
                      "bg-green-700": pros.type === "success",
                    },
                    {
                      "bg-red-700": pros.type === "error",
                    },
                    {
                      "bg-yellow-700": pros.type === "warning",
                    },
                    {
                      "bg-indigo-700": pros.type === "default" || pros.type === "",
                    },
                  )}
                >
                  <svg
                    className="h-6 w-6 text-white "
                    xmlns="http://www.w3.org/2000/svg"
                    fill="none"
                    viewBox="0 0 24 24"
                    stroke="currentColor"
                  >
                    <path
                      strokeLinecap="round"
                      strokeLinejoin="round"
                      strokeWidth="2"
                      d="M11 5.882V19.24a1.76 1.76 0 01-3.417.592l-2.147-6.15M18 13a3 3 0 100-6M5.436 13.683A4.001 4.001 0 017 6h1.832c4.1 0 7.625-1.234 9.168-3v14c-1.543-1.766-5.067-3-9.168-3H7a3.988 3.988 0 01-1.564-.317z"
                    />
                  </svg>
                </span>
                <p
                  className={classname(
                    " font-medium text-white truncate ml-3"
                 
                 
                  )}
                >
                  <span className="md:hidden">{pros.title}</span>
                  <span className="hidden md:inline">{pros.text}</span>
                </p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </React.Fragment>
  );
};

export default Message;
