const colors = require("tailwindcss/colors");
const defaultTheme = require("tailwindcss/defaultTheme");

module.exports = {
  purge: ["./pages/**/*.{js,ts,jsx,tsx}", "./components/**/*.{js,ts,jsx,tsx}"],
  darkMode: false, // or 'media' or 'class'
  theme: {
    extend: {
      colors: {
        gold: {
          DEFAULT: "#B4881C",
          50: "#F5E6C0",
          100: "#F2DDAA",
          200: "#EBCB7E",
          300: "#E4BA52",
          400: "#DDA826",
          500: "#B4881C",
          600: "#886715",
          700: "#5C450E",
          800: "#302407",
          900: "#030301",
        },
        "gray-mod": {
          DEFAULT: "#6E6A69",
          50: "#DFDEDD",
          100: "#D3D1D0",
          200: "#BAB7B6",
          300: "#A19D9C",
          400: "#888382",
          500: "#6E6A69",
          600: "#545150",
          700: "#3A3837",
          800: "#201F1E",
          900: "#060505",
        },
        "blue-mod": {
          DEFAULT: "#0B3B5D",
          50: "#61B3EC",
          100: "#4AA8EA",
          200: "#1D92E4",
          300: "#1675B8",
          400: "#10588B",
          500: "#0B3B5D",
          600: "#061E2F",
          700: "#000102",
          800: "#000000",
          900: "#000000",
        },
      },
      fontFamily: {
        sans: ["Inter var", ...defaultTheme.fontFamily.sans],
      },
    },
  },
  variants: {
    extend: {},
  },
  plugins: [
    require("@tailwindcss/forms"),
    require("@tailwindcss/typography"),
    require("@tailwindcss/aspect-ratio"),
    require("@tailwindcss/line-clamp"),
  ],
};
