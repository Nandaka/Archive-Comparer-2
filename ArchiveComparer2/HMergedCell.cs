using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace ArchiveComparer2
{
    /// <summary>
    /// http://forums.codeguru.com/showthread.php?415930-DataGridView-Merging-Cells
    /// </summary>
    public class HMergedCell : DataGridViewTextBoxCell
    {
        private int m_nLeftColumn = 0;
        private int m_nRightColumn = 0;

        /// <summary>
        /// Column Index of the left-most cell to be merged.
        /// This cell controls the merged text.
        /// </summary>
        public int LeftColumn
        {
            get
            {
                return m_nLeftColumn;
            }
            set
            {
                m_nLeftColumn = value;
            }
        }

        /// <summary>
        /// Column Index of the right-most cell to be merged
        /// </summary>
        public int RightColumn
        {
            get
            {
                return m_nRightColumn;
            }
            set
            {
                m_nRightColumn = value;
            }
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
            this.DataGridView.SuspendLayout();
            try
            {
                int mergeindex = ColumnIndex - m_nLeftColumn;
                int i;
                int nWidth;
                int nWidthLeft;
                string strText;

                using (Brush backColorBrush = new SolidBrush(cellStyle.BackColor), selectedBrush = new SolidBrush(cellStyle.SelectionBackColor))
                {
                    using (Pen gridLinePen = new Pen(DataGridView.GridColor))
                    {
                        // Draw the separator for rows
                        //graphics.DrawLine(new Pen(new SolidBrush(DataGridView.GridColor)), cellBounds.Left, cellBounds.Bottom - 1, cellBounds.Right, cellBounds.Bottom - 1);

                        // Draw the right vertical line for the cell
                        if (ColumnIndex == m_nRightColumn)
                            graphics.DrawLine(gridLinePen, cellBounds.Right - 1, cellBounds.Top, cellBounds.Right - 1, cellBounds.Bottom);
                    }

                    // Draw the background
                    if (Selected) graphics.FillRectangle(selectedBrush, cellBounds);
                    else graphics.FillRectangle(backColorBrush, cellBounds);

                    // Draw the text
                    RectangleF rectDest = RectangleF.Empty;
                    StringFormat sf = new StringFormat();
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Near;
                    sf.Trimming = StringTrimming.EllipsisCharacter;

                    // Determine the total width of the merged cell
                    nWidth = 0;
                    for (i = m_nLeftColumn; i <= m_nRightColumn; i++)
                        nWidth += this.OwningRow.Cells[i].Size.Width;

                    // Determine the width before the current cell.
                    nWidthLeft = 0;
                    for (i = m_nLeftColumn; i < ColumnIndex; i++)
                        nWidthLeft += this.OwningRow.Cells[i].Size.Width;

                    // Retrieve the text to be displayed
                    strText = this.OwningRow.Cells[m_nLeftColumn].Value.ToString();

                    rectDest = new RectangleF(cellBounds.Left - nWidthLeft, cellBounds.Top, nWidth, cellBounds.Height);
                    graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
                    graphics.DrawString(strText, new Font(cellStyle.Font, FontStyle.Bold), Brushes.White, rectDest, sf);
                }
                
                graphics.ResetClip();
                this.DataGridView.ResumeLayout();
                
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
        }

    }
}
