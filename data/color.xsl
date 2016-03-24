<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:my="whatever" exclude-result-prefixes="my">

<!--
This is an important transformation. It will generate a colored HTML version of the "name" property by combining name+color properties from the original XML.
They are combined in a new property named "coloredName". Make sure you do not have extra spaces or newlines withing attributes.
   
If an entry did not have an explicit color attribute to it I will assign a color to if according to the MOD values of its id

Problem: too slow. I will use hardcoded class for colors

Last update: 2014-10-07
-->

  <xsl:output method="xml" indent="yes"/>

  <my:color>
    <color background="dark">AliceBlue</color>
    <color background="dark">AntiqueWhite</color>
    <color background="dark">Aqua</color>
    <color background="dark">Aquamarine</color>
    <color background="dark">Azure</color>
    <color background="dark">Beige</color>
    <color background="dark">Bisque</color>
    <color background="light">Black</color>
    <color background="dark">BlanchedAlmond</color>
    <color background="light">Blue</color>
    <color background="light">BlueViolet</color>
    <color background="light">Brown</color>
    <color background="dark">BurlyWood</color>
    <color background="light">CadetBlue</color>
    <color background="light">Chartreuse</color>
    <color background="light">Chocolate</color>
    <color background="dark">Coral</color>
    <color background="dark">CornFlowerBlue</color>
    <color background="dark">Cornsilk</color>
    <color background="light">Crimson</color>
    <color background="dark">Cyan</color>
    <color background="light">DarkBlue</color>
    <color background="light">DarkCyan</color>
    <color background="light">DarkGoldenrod</color>
    <color background="dark">DarkGray</color>
    <color background="light">DarkGreen</color>
    <color background="dark">DarkKhaki</color>
    <color background="light">DarkMagenta</color>
    <color background="light">DarkOliveGreen</color>
    <color background="dark">DarkOrange</color>
    <color background="light">DarkOrchid</color>
    <color background="light">DarkRed</color>
    <color background="dark">DarkSalmon</color>
    <color background="dark">DarkSeaGreen</color>
    <color background="light">DarkSlateBlue</color>
    <color background="light">DarkSlateGray</color>
    <color background="light">DarkTurquoise</color>
    <color background="light">DarkViolet</color>
    <color background="light">DeepPink</color>
    <color background="light">DeepSkyBlue</color>
    <color background="light">DimGray</color>
    <color background="light">DodgerBlue</color>
    <color background="light">Firebrick</color>
    <color background="dark">FloralWhite</color>
    <color background="light">ForestGreen</color>
    <color background="dark">Fuchsia</color>
    <color background="dark">Gainsboro</color>
    <color background="dark">GhostWhite</color>
    <color background="dark">Gold</color>
    <color background="dark">Goldenrod</color>
    <color background="light">Gray</color>
    <color background="light">Green</color>
    <color background="dark">GreenYellow</color>
    <color background="dark">Honeydew</color>
    <color background="dark">HotPink</color>
    <color background="light">IndianRed</color>
    <color background="light">Indigo</color>
    <color background="dark">Ivory</color>
    <color background="dark">Khaki</color>
    <color background="dark">Lavender</color>
    <color background="dark">LavenderBlush</color>
    <color background="light">LawnGreen</color>
    <color background="dark">LemonChiffon</color>
    <color background="dark">LightBlue</color>
    <color background="dark">LightCoral</color>
    <color background="dark">LightCyan</color>
    <color background="dark">LightGoldenRodYellow</color>
    <color background="dark">LightGray</color>
    <color background="dark">LightGreen</color>
    <color background="dark">LightPink</color>
    <color background="dark">LightSalmon</color>
    <color background="light">LightSeaGreen</color>
    <color background="dark">LightSkyBlue</color>
    <color background="light">LightSlateGray</color>
    <color background="dark">LightSteelBlue</color>
    <color background="dark">LightYellow</color>
    <color background="light">Lime</color>
    <color background="light">LimeGreen</color>
    <color background="dark">Linen</color>
    <color background="dark">Magenta</color>
    <color background="light">Maroon</color>
    <color background="dark">MediumAquamarine</color>
    <color background="light">MediumBlue</color>
    <color background="dark">MediumOrchid</color>
    <color background="dark">MediumPurple</color>
    <color background="light">MediumSeaGreen</color>
    <color background="light">MediumSlateBlue</color>
    <color background="light">MediumSpringGreen</color>
    <color background="dark">MediumTurquoise</color>
    <color background="light">MediumVioletRed</color>
    <color background="light">MidnightBlue</color>
    <color background="dark">MintCream</color>
    <color background="dark">MistyRose</color>
    <color background="dark">Moccasin</color>
    <color background="dark">NavajoWhite</color>
    <color background="light">Navy</color>
    <color background="dark">OldLace</color>
    <color background="light">Olive</color>
    <color background="light">OliveDrab</color>
    <color background="dark">Orange</color>
    <color background="light">OrangeRed</color>
    <color background="dark">Orchid</color>
    <color background="dark">PaleGoldenrod</color>
    <color background="dark">PaleGreen</color>
    <color background="dark">PaleTurquoise</color>
    <color background="dark">PaleVioletRed</color>
    <color background="dark">PapayaWhip</color>
    <color background="dark">PeachPuff</color>
    <color background="light">Peru</color>
    <color background="dark">Pink</color>
    <color background="dark">Plum</color>
    <color background="dark">PowderBlue</color>
    <color background="light">Purple</color>
    <color background="light">Red</color>
    <color background="dark">RosyBrown</color>
    <color background="light">RoyalBlue</color>
    <color background="light">SaddleBrown</color>
    <color background="dark">Salmon</color>
    <color background="dark">SandyBrown</color>
    <color background="light">SeaGreen</color>
    <color background="dark">SeaShell</color>
    <color background="light">Sienna</color>
    <color background="dark">Silver</color>
    <color background="dark">SkyBlue</color>
    <color background="light">SlateBlue</color>
    <color background="light">SlateGray</color>
    <color background="dark">Snow</color>
    <color background="light">SpringGreen</color>
    <color background="light">SteelBlue</color>
    <color background="dark">Tan</color>
    <color background="light">Teal</color>
    <color background="dark">Thistle</color>
    <color background="dark">Tomato</color>
    <color background="light">Transparent</color>
    <color background="dark">Turquoise</color>
    <color background="dark">Violet</color>
    <color background="dark">Wheat</color>
    <color background="dark">White</color>
    <color background="dark">WhiteSmoke</color>
    <color background="dark">Yellow</color>
    <color background="light">YellowGreen</color>
  </my:color>

  <xsl:template match="@*|node()">
    <xsl:copy>
      <xsl:choose>
        <xsl:when test="@color">
          <xsl:if test="@name">
            <xsl:attribute name="coloredName">&lt;span style="color:<xsl:value-of select="@color" />"&gt;<xsl:value-of select="@name" />&lt;/span&gt;</xsl:attribute>
          </xsl:if>
          <xsl:if test="@arabicName">
            <xsl:attribute name="arabicColoredName">&lt;span style="color:<xsl:value-of select="@color" />"&gt;<xsl:value-of select="@arabicName" />&lt;/span&gt;</xsl:attribute>
          </xsl:if>
          <xsl:if test="@shortName">
            <xsl:attribute name="shortColoredName">&lt;span style="color:<xsl:value-of select="@color" />"&gt;<xsl:value-of select="@shortName" />&lt;/span&gt;</xsl:attribute>
          </xsl:if>
          <xsl:if test="@arabicShortName">
            <xsl:attribute name="arabicShortColoredName">&lt;span style="color:<xsl:value-of select="@color" />"&gt;<xsl:value-of select="@arabicShortName" />&lt;/span&gt;</xsl:attribute>
          </xsl:if>
        </xsl:when>
        <xsl:otherwise>

          <!-- below: color is not explicitly defined here we will assign a color according to the position of the node -->
          <xsl:variable name="pos" select="position() mod count(document('')/*/my:color/color[@background='light']) +1"/>

          <xsl:if test="@name">
            <xsl:attribute name="coloredName">&lt;span style="color:<xsl:value-of select="document('')/*/my:color/color[@background='light'][$pos]" />"&gt;<xsl:value-of select="@name" />&lt;/span&gt;</xsl:attribute>
          </xsl:if>
          <xsl:if test="@arabicName">
            <xsl:attribute name="arabicColoredName">&lt;span style="color:<xsl:value-of select="document('')/*/my:color/color[@background='light'][$pos]" />"&gt;<xsl:value-of select="@arabicName" />&lt;/span&gt;</xsl:attribute>
          </xsl:if>
          <xsl:if test="@shortName">
            <xsl:attribute name="shortColoredName">&lt;span style="color:<xsl:value-of select="document('')/*/my:color/color[@background='light'][$pos]" />"&gt;<xsl:value-of select="@shortName" />&lt;/span&gt;</xsl:attribute>
          </xsl:if>
          <xsl:if test="@arabicShortName">
            <xsl:attribute name="arabicShortColoredName">&lt;span style="color:<xsl:value-of select="document('')/*/my:color/color[@background='light'][$pos]" />"&gt;<xsl:value-of select="@arabicShortName" />&lt;/span&gt;</xsl:attribute>
          </xsl:if>

        </xsl:otherwise>
      </xsl:choose>

      <!-- below: this builds a longName attribute -->
      <xsl:choose>
        <xsl:when test="name() = local-name(parent::*) and @name and ../@name">
          <xsl:attribute name="longName">
            <xsl:value-of select="../@name" /> » <xsl:value-of select="@name" />
          </xsl:attribute>
          <xsl:if test="@color">
            <xsl:attribute name="longColoredName">&lt;span style="color:<xsl:value-of select="../@color" />"&gt;<xsl:value-of select="../@name" />&lt;/span&gt; »&amp;nbsp;&lt;span style="color:<xsl:value-of select="@color" />"&gt;<xsl:value-of select="@name" />&lt;/span&gt;</xsl:attribute>
          </xsl:if>
        </xsl:when>
        <xsl:when test="name() and @name">
          <xsl:attribute name="longName"><xsl:value-of select="@name" /></xsl:attribute>
          <xsl:if test="@color">
            <xsl:attribute name="longColoredName">&lt;span style="color:<xsl:value-of select="@color" />"&gt;<xsl:value-of select="@name" />&lt;/span&gt;</xsl:attribute>
          </xsl:if>
        </xsl:when>
        <xsl:otherwise>
        </xsl:otherwise>
      </xsl:choose>

      <xsl:choose>
        <xsl:when test="name() = local-name(parent::*) and @arabicName and ../@arabicName">
          <xsl:attribute name="arabicLongName"><xsl:value-of select="../@arabicName" /> » <xsl:value-of select="@arabicName" /></xsl:attribute>
          <xsl:if test="@color">
            <xsl:attribute name="arabicLongColoredName">&lt;span style="color:<xsl:value-of select="../@color" />"&gt;<xsl:value-of select="../@arabicName" />&lt;/span&gt; »&amp;nbsp;&lt;span style="color:<xsl:value-of select="@color" />"&gt;<xsl:value-of select="@arabicName" />&lt;/span&gt;</xsl:attribute>
          </xsl:if>
        </xsl:when>
        <xsl:when test="name() and @arabicName">
          <xsl:attribute name="arabicLongName"><xsl:value-of select="@arabicName" /></xsl:attribute>
          <xsl:if test="@color">
            <xsl:attribute name="arabicLongColoredName">&lt;span style="color:<xsl:value-of select="@color" />"&gt;<xsl:value-of select="@arabicName" />&lt;/span&gt;</xsl:attribute>
          </xsl:if>
        </xsl:when>
        <xsl:otherwise>
        </xsl:otherwise>
      </xsl:choose>

      <xsl:apply-templates select="@*|node()"/>

    </xsl:copy>

  </xsl:template>

</xsl:stylesheet>
